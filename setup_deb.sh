rm -r freesims_0.0-0/usr/bin
mkdir freesims_0.0-0/usr/bin
mkdir freesims_0.0-0/usr/bin/FreeSims
cp -r FreeSims/FreeSims/bin/x86/Release/Content freesims_0.0-0/usr/bin/FreeSims/
cp -r FreeSims/FreeSims/bin/x86/Release/Language freesims_0.0-0/usr/bin/FreeSims/
cp FreeSims/FreeSims/bin/x86/Release/FreeSims.exe freesims_0.0-0/usr/bin/FreeSims/
cp FreeSims/FreeSims/bin/x86/Release/MonoGame.Framework.dll freesims_0.0-0/usr/bin/FreeSims/
cp FreeSims/FreeSims/bin/x86/Release/OpenTK.dll freesims_0.0-0/usr/bin/FreeSims/
cp FreeSims/FreeSims/bin/x86/Release/NVorbis.dll freesims_0.0-0/usr/bin/FreeSims/
cp FreeSims/FreeSims/Icon.ico freesims_0.0-0/usr/bin/FreeSims/
dpkg-deb --build freesims_0.0-0
mv freesims_0.0-0.deb Output/freesims_setup.deb