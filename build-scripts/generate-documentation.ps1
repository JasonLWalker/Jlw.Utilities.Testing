param([string]$sourcePath, [string]$outputPath)

Write-Host "===================== Generating Documentation Markdown ====================="
dotnet tool install xmldoc2markdown
dotnet xmldoc2md $sourcePath\Jlw.Utilities.Testing.dll --output $outputPath\Jlw.Utilities.Testing --member-accessibility-level public --github-pages --back-button
