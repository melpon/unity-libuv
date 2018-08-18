#!/bin/bash

set -ex

SDK_IOS_VERSION=11.4

SCRIPT_DIR=$(cd `dirname $0`; pwd)
cd $SCRIPT_DIR

VERSION=`cat ../VERSION`

DIST_DIR="$SCRIPT_DIR/dist"
TMP_DIR="$SCRIPT_DIR/tmp"

PLATFORM_IOS=`xcode-select -print-path`"/Platforms/iPhoneOS.platform/"
PLATFORM_IOS_SIM=`xcode-select -print-path`"/Platforms/iPhoneSimulator.platform/"
MIN_IOS_VERSION="5.0"

rm -rf "$TMP_DIR" || true
mkdir "$TMP_DIR"

git clone --depth=1 --branch=v$VERSION https://github.com/libuv/libuv.git $TMP_DIR/libuv

pushd $TMP_DIR/libuv

build_target() {
    local arch=$1
    local platform=$2
    local sdk=$3
    local extra_config=$4

    sh autogen.sh
    ./configure \
      --disable-shared \
      --enable-static \
      --with-pic \
      --disable-extra-programs \
      --disable-doc \
      $extra_config \
      CFLAGS=" \
        -Ofast \
        -flto \
        -fPIE \
        -arch ${arch} \
        -isysroot ${sdk} \
        -DTARGET_OS_IPHONE \
        -D__IPHONE_OS_VERSION_MIN_REQUIRED=50100 \
        -miphoneos-version-min=${MIN_IOS_VERSION} \
      " \
      LDFLAGS=" \
        -flto \
        -fPIE \
        -miphoneos-version-min=${MIN_IOS_VERSION} \
      "
    make -j4
    cp .libs/libuv.a "$TMP_DIR/libuv-${arch}.a"
    git clean -xdqf
}

build_target arm64 "${PLATFORM_IOS}" "${PLATFORM_IOS}/Developer/SDKs/iPhoneOS${SDK_IOS_VERSION}.sdk/" "--host=arm-apple-darwin"
build_target x86_64 "${PLATFORM_IOS_SIM}" "${PLATFORM_IOS_SIM}/Developer/SDKs/iPhoneSimulator${SDK_IOS_VERSION}.sdk/" "--host=x86_64-apple-darwin"

popd

pushd $TMP_DIR

# Create the universal binary
rm -rf "$DIST_DIR" || true
mkdir -p "$DIST_DIR/lib"
lipo -create libuv-arm64.a libuv-x86_64.a -output "${DIST_DIR}/lib/libuv.a"

popd

rm -rf $TMP_DIR
