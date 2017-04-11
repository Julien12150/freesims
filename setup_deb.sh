rm -r freesims_0.0-0/usr/share/freesims
mkdir -p freesims_0.0-0/usr/share/freesims
mkdir -p freesims_0.0-0/usr/share/pixmaps
cp -r FreeSims/FreeSims/bin/x86/Release/Content freesims_0.0-0/usr/share/freesims/
cp -r FreeSims/FreeSims/bin/x86/Release/Language freesims_0.0-0/usr/share/freesims/
cp FreeSims/FreeSims/bin/x86/Release/FreeSims.exe freesims_0.0-0/usr/share/freesims/
cp FreeSims/FreeSims/bin/x86/Release/MonoGame.Framework.dll freesims_0.0-0/usr/share/freesims/
cp FreeSims/FreeSims/bin/x86/Release/OpenTK.dll freesims_0.0-0/usr/share/freesims/
cp FreeSims/FreeSims/bin/x86/Release/NVorbis.dll freesims_0.0-0/usr/share/freesims/
cp FreeSims/FreeSims/Icon.ico freesims_0.0-0/usr/share/freesims/
cp FreeSims/FreeSims/Icon.svg freesims_0.0-0/usr/share/pixmaps/freesims.svg
dpkg-deb --build freesims_0.0-0
mkdir Output
mv freesims_0.0-0.deb Output/freesims_setup.deb
