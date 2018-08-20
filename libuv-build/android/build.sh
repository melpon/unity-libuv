#!/bin/bash

set -ex

if [ -z "$NDK_HOME" ]; then
  NDK_HOME=~/Library/Android/sdk/ndk-bundle
fi

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

source ./android-configure $NDK_HOME gyp 21
make BUILDTYPE=Release -C out

popd

rm -rf "$DIST_DIR" || true
mkdir -p "$DIST_DIR/lib"
cp "$TMP_DIR/libuv/out/Release/libuv.a" "$DIST_DIR/lib"

rm -rf "$TMP_DIR"
