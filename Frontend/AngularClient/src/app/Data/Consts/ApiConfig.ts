export class ApiConfig{
  public static readonly BaseUrl: string = "localhost:8080"
  public static readonly BaseHttpsUrl: string = `https://${ApiConfig.BaseUrl}`
  public static readonly BaseWssUrl: string = `wss://${ApiConfig.BaseUrl}`
}
