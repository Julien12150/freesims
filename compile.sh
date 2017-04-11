mcs -r:/usr/lib/mono/xbuild/MonoGame/v3.0/Assemblies/DesktopGL/MonoGame.Framework.dll,OpenTK.2.0.0/lib/net20/OpenTK.dll,/usr/lib/mono/4.5/Mono.Posix.dll,NVorbis.0.8.5.0/lib/NVorbis.dll,/usr/lib/mono/4.5/System.dll,/usr/lib/mono/4.5/System.Drawing.dll,/usr/lib/mono/4.5/System.Windows.Forms.dll -recurse:'FreeSims/FreeSims/*.cs' -out:FreeSims/FreeSims/bin/x86/Release/FreeSims.exe -win32icon:FreeSims/FreeSims/Icon.ico -pkg:gtk-sharp-2.0
cp /usr/lib/mono/xbuild/MonoGame/v3.0/Assemblies/DesktopGL/MonoGame.Framework.dll FreeSims/FreeSims/bin/x86/Release/
cp OpenTK.2.0.0/lib/net20/OpenTK.dll FreeSims/FreeSims/bin/x86/Release/
cp NVorbis.0.8.5.0/lib/NVorbis.dll FreeSims/FreeSims/bin/x86/Release/
cp -r FreeSims/FreeSims/Language FreeSims/FreeSims/bin/x86/Release
