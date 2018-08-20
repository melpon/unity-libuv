#!/bin/bash

set -ex

SCRIPT_DIR=$(cd `dirname $0`; pwd)
cd $SCRIPT_DIR

VERSION=`cat ../VERSION`

DIST_DIR="$SCRIPT_DIR/dist"
TMP_DIR="$SCRIPT_DIR/tmp"

rm -rf "$TMP_DIR" || true
mkdir "$TMP_DIR"

git clone --depth=1 --branch=v$VERSION https://github.com/libuv/libuv.git $TMP_DIR/libuv

pushd "$TMP_DIR/libuv"

git clone --depth=1 https://chromium.googlesource.com/external/gyp build/gyp

# for generating .bundle project
sed -i -e s/\'type\'.*/\'type\':\'loadable_module\',\'mac_bundle\':1,/ uv.gyp

./gyp_uv.py -f xcode

xcodebuild \
  -ARCHS="x86_64" \
  -project out/uv.xcodeproj \
  -configuration Release \
  -target libuv

# re-generate test project
git checkout uv.gyp
./gyp_uv.py -f xcode
xcodebuild \
  -ARCHS="x86_64" \
  -project out/test/test.xcodeproj \
  -configuration Release \
  -target run-tests \
  BUILD_DIR="$TMP_DIR/libuv/build/test" \

./build/test/Release/run-tests

popd

rm -rf "$DIST_DIR" || true
mkdir -p "$DIST_DIR/lib"
cp -r "$TMP_DIR/libuv/build/Release/libuv.bundle" "$DIST_DIR/libuv.bundle"

rm -rf "$TMP_DIR"
