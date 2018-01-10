global.config = {
    name:           'WellFired.Guacamole',
    slnPath:        'solution/WellFired.Guacamole.sln',
    unityAssets:    'unity/Assets',
    unityBin:       'unity/Assets/Code',
    unityEditorBin: 'unity/Assets/Code/Editor',
    integrationDlls:'solution/Tests/Core/*/bin/Debug/*.Integration.dll',
    testDlls:       'solution/Tests/Core/*/bin/Debug/*.Unit.dll',
    nugetConfig:    'solution/NuGet.config',
    basecsproj:     'solution/WellFired.Guacamole/WellFired.Guacamole.csproj',
    unityEditor:    'solution/WellFired.Guacamole.Unity.Editor/WellFired.Guacamole.Unity.Editor.csproj',
    unityRuntime:   'solution/WellFired.Guacamole.Unity.Runtime/WellFired.Guacamole.Unity.Runtime.csproj',
    doxyConfig:     'documentation/doxyconf',
    sphinxInputDir: 'documentation/xml',
    sphinxOutputDir:'documentation/sphinx',
    sphinxProjectName: 'dotGuacamole'
}

module.exports = {
    config: -> return config

    wtask: ->
        t = task.apply(global, arguments)

        t.addListener 'start', ->
            WellFired.logStart(this);

        return t
}