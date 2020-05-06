Function KillThatProcess([string] $process)
{
    $allProcesses = Get-Process $process
    if ($allProcesses.Count -eq 1)
    {
        $answer = Read-Host “There is" $allProcesses.Count "process with the name '" $process "', proceed? Y/N”
    }
    else
    {
        $answer = Read-Host “There are" $allProcesses.Count "processes with the name '"  $process "', proceed? Y/N”
    }
    if ($answer.ToLower() -eq "y")
    {
        get-process $process | spps
        Write-Host -ForegroundColor Green "All '" $process "' processes were stopped"
    }
    else
    {
        Write-Host -ForegroundColor Red "No '" $process "' processes were stopped"
    }

    
}

Function Bamboozle($path = (Get-Location))
{
    (65..90) | Get-Random -Count 1 | % {[char] $_}
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "The initial randonly generated letter was '"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "D"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "' as in: '"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "D"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "a "
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "D"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "isney channel "
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "d"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "islayed "
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "D"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "ende from "
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "D"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "ragonball Z "
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "d"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "ancing "
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "d"
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Blue "aintily to "
    Write-Host -NoNewline -BackgroundColor White -ForegroundColor Red "d"
    Write-Host -BackgroundColor White -ForegroundColor Blue "ubstep'"
    $listOfFiles = Get-ChildItem -Path $path -File -Name "*d*"
    if($listOfFiles -eq $null)
    {
        Write-Host -ForegroundColor Red "No files containing the letter 'D' were found"
    }
    else
    {
        Set-Location $path
        Remove-Item $listOfFiles -WhatIf
    }
}

