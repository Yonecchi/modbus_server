using System;
using System.Net;
using System.Net.Sockets;

namespace modbus_server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 502); // Modbusのデフォルトポートは502
            listener.Start();

            Console.WriteLine("Modbus TCP Server started.");

            using (TcpClient client = listener.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // データの処理とレスポンスの生成
                    // Modbusプロトコルに従ってレスポンスデータを作成する
                    byte[] responseData = GenerateModbusResponse(buffer, bytesRead);

                    // レスポンスをクライアントに送信
                    stream.Write(responseData, 0, responseData.Length);
                }
            }

            listener.Stop();
        }
        static byte[] GenerateModbusResponse(byte[] requestData, int length)
        {
            Console.WriteLine("serverdata: " + BitConverter.ToString(requestData));
            // ここでModbusのリクエストデータを解析し、適切なレスポンスデータを生成するロジックを実装
            // responseDataに生成したレスポンスデータを格納して返す
            byte[] responseData = new byte[length];
            Array.Copy(requestData, responseData, length);
            return responseData;
        }
    }
}