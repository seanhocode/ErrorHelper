# [release.yml](./release-example.yml)做了什麼
- ``on.push.tags``: 'v*'：只要推 ``v`` 開頭的 tag 就當成發佈意圖
- ``permissions.contents: write``：否則不能建立 Release
- ``Read version from tag``：把 ``v1.2.3`` 轉成 ``1.2.3``，供 -p:Version= 和檔名使用
- ``Publish``：
    - ``-r win-x64``：指定 Windows x64；要 ARM 可改 ``win-arm64``；要 32bit 可 ``win-x86``
    - ``--self-contained true`` + ``-p:PublishSingleFile=true``：單檔自含，使用者無需 .NET 8
    - ``-p:Version=``：把檔案版本、產品版本對齊 tag，避免版本漂移
- ``Zip artifacts``：把發佈結果壓成 ``YourTool-1.2.3-win-x64.zip``，易於識別
- ``Create GitHub Release``：用官方 token 直接在同 repo 建 Release，免個人 PAT
- ``generate_release_notes``: true 會自動摘要變更（以 PR/Commit 為來源）

# [releasev2.yml](../.github/workflows/release.yml)做了什麼
## ``dotnet publish``
- ``-c Release`` → 使用 Release 組態
- ``-r win-x64`` → 編譯成 Windows x64 的目標（Framework dependent）
- ``--self-contained false`` → 不是自含，檔案會很小，但使用者必須裝 .NET 8 runtime
- ``-p:PublishSingleFile=false`` → 不要合併成單檔（會輸出一堆 dll/exe）
- ``-o Release/build`` → 把輸出檔案放到 repo 根目錄下的 ``Release/build``
## ``Compress-Archive``
- 把``Release/build`` 的檔案壓成 zip，命名包含版本號，丟到 ``artifacts``，再用 ``action-gh-release`` 上傳