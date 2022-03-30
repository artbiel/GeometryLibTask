using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GeometryLib.SourceGenerator
{
    [Generator]
    public class AreaUtilsGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = new StringBuilder(@"
using GeometryLib.Helpers;

namespace AreaLib.Utils
{
    public static class AreaUtils
    {");
            var syntaxTrees = context.Compilation.SyntaxTrees;
            foreach (var syntaxTree in syntaxTrees)
            {
                // Get a class derived from Shape
                var shapeTypeDeclarations = syntaxTree.GetRoot()
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Where(t => t.BaseList?.Types.Any(b => b.Type?.ToString().Equals("Shape") ?? false) ?? false).ToArray();

                foreach (var shapeTypeDeclaration in shapeTypeDeclarations)
                {
                    var model = context.Compilation.GetSemanticModel(syntaxTree);

                    // Get class name
                    var shapeName = shapeTypeDeclaration.Identifier.ToString();

                    var getAreaMethod = shapeTypeDeclaration.ChildNodes()
                        .OfType<MethodDeclarationSyntax>()
                        .First(node => node.Identifier.ToString().Equals("GetArea"));

                    var allProperties = model.LookupSymbols(getAreaMethod.Span.Start).Where(s => s.Kind == SymbolKind.Property).Select(p => p.Name).ToArray();

                    var propertiesUsedInMethod = getAreaMethod.DescendantNodes()
                        .OfType<IdentifierNameSyntax>()
                        .Select(id => id.Identifier.ValueText)
                        .Where(id => allProperties.Contains(id))
                        .Distinct().ToArray();

                    var getAreaMethodSymbol = model.GetSymbolInfo(getAreaMethod);

                    // Get constructor which initialize all properties used in GetArea method
                    var constructor = shapeTypeDeclaration.ChildNodes()
                        .OfType<ConstructorDeclarationSyntax>()
                        .First(c => !propertiesUsedInMethod.Except(c.Body.Statements
                            .OfType<ExpressionStatementSyntax>()
                            .Select(e => e.Expression)
                            .OfType<AssignmentExpressionSyntax>()
                            .Select(e => (e.Left as IdentifierNameSyntax).Identifier.ValueText)
                            ).Any());

                    var constructorParameters = constructor.ParameterList.Parameters.ToFullString();

                    var constructorStatememnts = constructor.Body.Statements
                        .OfType<ExpressionStatementSyntax>();
                    var methodCheckAndAssignStatememnts = new StringBuilder();
                    foreach (var statement in constructorStatememnts)
                    {
                        if (statement.Expression is AssignmentExpressionSyntax)
                            // Prepend var if it's assignment
                            methodCheckAndAssignStatememnts.Append($@"
            var {statement.Expression.ToFullString().TrimStart()};");
                            else
                                methodCheckAndAssignStatememnts.Append($@"
            {statement.Expression.ToFullString().TrimStart()};");

                    }

                    // Get method body
                    // Append return if it's lambda
                    var body = getAreaMethod.Body != null ?
                        getAreaMethod.Body.Statements.ToFullString() :
                        $"return {getAreaMethod.ExpressionBody.Expression.ToFullString()};";

                    sourceBuilder.Append($@"
        public static double Get{shapeName}Area({constructorParameters})
        {{
            {methodCheckAndAssignStatememnts.ToString().TrimStart()} 
            {body.Trim()}
        }}");                    
                }
            }
            sourceBuilder.Append(@"
    }
}
");
            context.AddSource("AreaUtils", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//                Debugger.Launch();
//#endif
        }
    }
}

