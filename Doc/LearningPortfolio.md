# 20250909
## DI
把「誰要用誰」的關係，從各處 new 實作，改成由一個「組裝中心（Composition Root）」統一建立與分配。
優點：容易替換實作（真實/測試）、容易測試、減少耦合。
在 .NET 世界，我們用 Microsoft.Extensions.DependencyInjection 這個官方套件來做

## 分層
### Core
### Infrastructure
### App、Web
### Tool