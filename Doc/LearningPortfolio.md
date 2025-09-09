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

## C#
### record
record 是 C# 9 引入的一種 特殊類別（class），設計目的是：
預設就是 值相等比較（by-value equality），不是物件參考。
適合拿來當 資料攜帶型別 (DTO / Settings / Config / Event)。
預設會有 Deconstruct、with 等功能，很適合「不可變資料」。
### sealed
sealed 關鍵字的意思是：禁止被繼承。
你可以把一個 class 或 record 標記成 sealed。
用途：
    讓編譯器做更好的最佳化（因為它確定沒有人能 override 它）。
    避免子類別繼承後亂改等號比較、不可變特性。
### abstract
在 C# 裡，abstract 可以用在 類別 和 方法/屬性，意思是「抽象的，必須由子類別去實作」。
抽象類別 (abstract class)
不能被直接 new 出來。
可以包含：
    抽象成員（沒有實作，子類別必須 override）
    一般成員（有實作，子類別可以直接繼承使用）