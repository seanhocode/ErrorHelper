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

---

# 20250910
## C#
### ConcurrentBag<T>
在 System.Collections.Concurrent 命名空間下提供的一個 執行緒安全 (thread-safe) 的集合容器，設計用來讓多個執行緒可以同時存取資料而不需要額外的鎖定 (lock)
特點
    無序 (Unordered)
        ConcurrentBag 是一個「袋子 (bag)」，元素之間沒有固定順序。
        當你 Add() 和 TryTake() 時，無法保證 FIFO（Queue）或 LIFO（Stack）的順序。
    高效能的並行存取
        為了避免多執行緒搶同一個資源造成效能瓶頸，它採用了 分區 (partitioned) 設計。
        每個執行緒會維護一個「本地存放區」，取資料時會先從自己的本地區取；如果拿不到，才會去別的執行緒的區域偷資料。
    適合「多進多出」的情境
        多個執行緒同時加入 (Add) 和取出 (TryTake) 元素時，不需要自己加 lock。
        但如果只是「單一生產、多消費」或「多生產、單一消費」，可能用 ConcurrentQueue 或 ConcurrentStack 更合適。
### Parallel.ForEach
Parallel.ForEach 是 .NET 平行程式設計 (Parallel Programming) 裡的一個方法，位於 System.Threading.Tasks 命名空間下。
它的作用是：
    在多核心 CPU 上，將一個可列舉的集合（IEnumerable / IList）分割成多個部分，並行執行迴圈內容，達到加速效果。