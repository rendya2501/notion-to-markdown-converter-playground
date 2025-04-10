// Program.cs - エントリポイント
using NotionMarkdownConverter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notion.Client;
using NotionMarkdownConverter.Configuration;

// DIコンテナの設定
var services = new ServiceCollection();

// ロギングの設定
services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Information);
});

// コマンドライン引数から設定を取得
var config = AppConfiguration.FromCommandLine(args);
services.AddSingleton(config);

// NotionClientの登録
services.AddSingleton<INotionClient>(provider =>
    NotionClientFactory.Create(new ClientOptions
    {
        AuthToken = config.NotionAuthToken
    }));

// サービスの登録
services.AddSingleton<INotionClientWrapper, NotionClientWrapper>();
services.AddSingleton<IFrontmatterGenerator, FrontmatterGenerator>();
services.AddSingleton<IContentGenerator, ContentGenerator>();
services.AddSingleton<IMarkdownGenerator, MarkdownGenerator>();
services.Configure<ImageDownloaderOptions>(options =>
{
    options.MaxRetryCount = 3;
    options.RetryDelayMilliseconds = 1000;
    options.MaxConcurrentDownloads = 4;
    options.TimeoutSeconds = 30;
    options.SkipExistingFiles = true;
});
services.AddSingleton<IImageProcessor, ImageProcessor>();
services.AddSingleton<INotionExporter, NotionExporter>();

// サービスプロバイダーの構築
var serviceProvider = services.BuildServiceProvider();

// サービスの取得と実行
var exporter = serviceProvider.GetRequiredService<INotionExporter>();
await exporter.ExportPagesAsync();

// リソースの解放
if (serviceProvider is IDisposable disposable)
{
    disposable.Dispose();
}




//// コマンドライン引数から設定を取得
//var config = AppConfiguration.FromCommandLine(args);
//var exporter = new NotionExporter(config);
//await exporter.ExportPagesAsync();


// NotionClientの作成
// プロパティの取得
// ページの取得
// プロパティとページの情報からマークダウンの作成
// 後始末処理

// マークダウン変換処理をライブラリとして捉える

