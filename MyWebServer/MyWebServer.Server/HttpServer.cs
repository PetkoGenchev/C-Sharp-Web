﻿using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        public HttpServer(string ipAddress,int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);

            this.port = port;

            listener = new TcpListener(this.ipAddress, this.port);
        }

        public async Task Start()
        {

            this.listener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Listening for requestes...");

            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await this.ReadRequest(networkStream);

                Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                await WriteResponse(networkStream);

                var content = @"
<html>
    <head>
        <link rel=""icon"" href=""data:,"">
    </head>
    <body>
        Hello from the server!
    </body>
</html>";
                var contentLength = Encoding.UTF8.GetByteCount(content);

                var response = $@"
HTTP/1.1 200 OK
Server: My Web Server
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/html; charset=UTF-8

{content}";

                var responseBytes = Encoding.UTF8.GetBytes(response);
                await networkStream.WriteAsync(responseBytes);

                connection.Close();


            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var requestBuilder = new StringBuilder();

            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            return requestBuilder.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream)
        {

        }
    }
}
