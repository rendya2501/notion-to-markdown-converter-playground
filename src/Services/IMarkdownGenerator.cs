using NotionMarkdownConverter.Models;

namespace NotionMarkdownConverter.Services;

/// <summary>
/// マークダウンを生成するインターフェース
/// </summary>
public interface IMarkdownGenerator
{
    /// <summary>
    /// マークダウンを生成します。
    /// </summary>
    /// <param name="pageProperty"></param>
    /// <returns></returns>
    Task<string> GenerateMarkdownAsync(PageProperty pageProperty);
}
