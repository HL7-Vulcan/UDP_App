#!/bin/bash
# Azure App Service startup script
# Installs Node.js and fsh-sushi into /home (the only persistent, writable volume)
# This runs in the APP container (not the Kudu container), so node must be installed here.

set -e

NODE_VERSION="20"
NODE_DIR="/home/node"
NODE_BIN="$NODE_DIR/bin"
SUSHI_SCRIPT="/home/bin/sushi"

echo "[startup] Checking for node..."

# If node already installed and working, skip
if "$NODE_BIN/node" --version > /dev/null 2>&1; then
    echo "[startup] node already installed: $("$NODE_BIN/node" --version)"
else
    echo "[startup] Installing Node.js $NODE_VERSION LTS..."
    mkdir -p "$NODE_DIR"
    cd /tmp

    # Download node binary tarball (no npm needed, just the binary)
    ARCH="x64"
    NODE_TAR="node-v${NODE_VERSION}-linux-${ARCH}.tar.gz"
    NODE_URL="https://nodejs.org/dist/latest-v${NODE_VERSION}.x/${NODE_TAR}"

    # Get the actual latest version number first
    LATEST=$(curl -sS "https://nodejs.org/dist/latest-v${NODE_VERSION}.x/" \
        | grep -oP "node-v[\d.]+-linux-${ARCH}\.tar\.gz" | head -1)

    if [ -n "$LATEST" ]; then
        curl -sS "https://nodejs.org/dist/latest-v${NODE_VERSION}.x/$LATEST" -o "$LATEST"
        tar -xzf "$LATEST" --strip-components=1 -C "$NODE_DIR"
        rm "$LATEST"
    fi

    if "$NODE_BIN/node" --version > /dev/null 2>&1; then
        echo "[startup] Node installed: $("$NODE_BIN/node" --version)"
    else
        echo "[startup] ERROR: Node installation failed"
    fi
fi

echo "[startup] Checking for sushi..."
if [ ! -f "$SUSHI_SCRIPT" ]; then
    echo "[startup] Installing fsh-sushi..."
    export PATH="$NODE_BIN:$PATH"
    "$NODE_BIN/npm" install -g fsh-sushi --prefix /home
fi

if [ -f "$SUSHI_SCRIPT" ]; then
    echo "[startup] sushi found at $SUSHI_SCRIPT"
else
    echo "[startup] WARNING: sushi not found after install attempt"
fi

echo "[startup] Done. Starting app..."
