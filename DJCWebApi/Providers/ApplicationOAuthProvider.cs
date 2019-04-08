System.ArgumentException: 已添加项。字典中的关键字:“String[] {3}textArray1”所添加的关键字:“String[] {3}textArray1”
   在 System.Collections.Hashtable.Insert(Object key, Object nvalue, Boolean add)
   在 Reflector.CodeModel.Visitor.Cloner.TransformVariableDeclaration(IVariableDeclaration value)
   在 Reflector.CodeModel.Visitor.Cloner.TransformVariableDeclarationExpression(IVariableDeclarationExpression value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformExpression(IExpression value)
   在 Reflector.CodeModel.Visitor.Cloner.TransformAssignExpression(IAssignExpression value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformExpression(IExpression value)
   在 Reflector.CodeModel.Visitor.Cloner.TransformExpressionStatement(IExpressionStatement value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformStatement(IStatement value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformStatementCollection(IStatementCollection value)
   在 Reflector.CodeModel.Visitor.Cloner.TransformBlockStatement(IBlockStatement value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformStatement(IStatement value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformStatementCollection(IStatementCollection value)
   在 Reflector.CodeModel.Visitor.Cloner.TransformBlockStatement(IBlockStatement value)
   在 Reflector.Disassembler.Optimizer.TransformMethodDeclaration(IMethodDeclaration value)
   在 Reflector.Disassembler.Disassembler.TransformMethodDeclaration(IMethodDeclaration value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformMethodDeclarationCollection(IMethodDeclarationCollection methods)
   在 Reflector.Disassembler.Disassembler.TransformTypeDeclaration(ITypeDeclaration value)
   在 Reflector.Application.Translator.TranslateTypeDeclaration(ITypeDeclaration value, Boolean memberDeclarationList, Boolean methodDeclarationBody)
   在 Reflector.Application.FileDisassembler.WriteTypeDeclaration(ITypeDeclaration typeDeclaration, String sourceFile, ILanguageWriterConfiguration languageWriterConfiguration)
namespace DJCWebApi.Providers
{
}

