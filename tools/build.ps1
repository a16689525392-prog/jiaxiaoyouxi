param(
    [Parameter(Mandatory=$true)][string]$ManagedDirectory,
    [string]$OutputDirectory = (Join-Path $PSScriptRoot '..\build'),
    [string]$SourceDirectory = (Join-Path $PSScriptRoot '..\src\Game')
)
$ErrorActionPreference = 'Stop'
if ($PSVersionTable.PSEdition -ne 'Core') { throw 'Run this script with PowerShell 7 (pwsh).' }
[void][Reflection.Assembly]::LoadFrom((Join-Path $PSHOME 'Microsoft.CodeAnalysis.dll'))
[void][Reflection.Assembly]::LoadFrom((Join-Path $PSHOME 'Microsoft.CodeAnalysis.CSharp.dll'))
$sourceDirectory = $SourceDirectory
$trees = [System.Collections.Generic.List[Microsoft.CodeAnalysis.SyntaxTree]]::new()
$parseOptions = [Microsoft.CodeAnalysis.CSharp.CSharpParseOptions]::Default.WithLanguageVersion([Microsoft.CodeAnalysis.CSharp.LanguageVersion]::CSharp7_3)
foreach ($file in Get-ChildItem -LiteralPath $sourceDirectory -Filter '*.cs' -Recurse | Sort-Object FullName) {
    $trees.Add([Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree]::ParseText(
        [IO.File]::ReadAllText($file.FullName), $parseOptions, $file.FullName,
        [Text.Encoding]::UTF8, [Threading.CancellationToken]::None))
}
$references = [System.Collections.Generic.List[Microsoft.CodeAnalysis.MetadataReference]]::new()
foreach ($file in Get-ChildItem -LiteralPath $ManagedDirectory -Filter '*.dll' | Where-Object Name -NE 'Assembly-CSharp.dll') {
    $references.Add([Microsoft.CodeAnalysis.MetadataReference]::CreateFromFile(
        $file.FullName, [Microsoft.CodeAnalysis.MetadataReferenceProperties]::Assembly, $null))
}
$options = [Microsoft.CodeAnalysis.CSharp.CSharpCompilationOptions]::new([Microsoft.CodeAnalysis.OutputKind]::DynamicallyLinkedLibrary)
$options = $options.WithAllowUnsafe($true).WithOptimizationLevel([Microsoft.CodeAnalysis.OptimizationLevel]::Release)
$options = $options.WithAssemblyIdentityComparer([Microsoft.CodeAnalysis.DesktopAssemblyIdentityComparer]::Default)
$compilation = [Microsoft.CodeAnalysis.CSharp.CSharpCompilation]::Create('Assembly-CSharp', $trees, $references, $options)
[void][IO.Directory]::CreateDirectory($OutputDirectory)
$outputPath = Join-Path $OutputDirectory 'Assembly-CSharp.dll'
$stream = [IO.File]::Create($outputPath)
try { $result = $compilation.Emit($stream) } finally { $stream.Dispose() }
$diagnostics = @($result.Diagnostics | ForEach-Object ToString)
$diagnostics | Set-Content -LiteralPath (Join-Path $OutputDirectory 'compiler.log') -Encoding utf8
if (-not $result.Success) {
    $result.Diagnostics | Where-Object Severity -EQ Error | Select-Object -First 50 | ForEach-Object ToString
    throw 'Compilation failed. See build/compiler.log.'
}
Write-Output "BUILT $outputPath ($($trees.Count) C# source files)"
