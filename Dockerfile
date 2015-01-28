FROM mono:latest
ADD WebSocketSpike WebSocketSpike
RUN xbuild /WebSocketSpike/WebSocketSpike.sln

EXPOSE 8080
CMD mono /WebSocketSpike/WebSocketSpike.LocalWebServer/bin/Debug/WebSocketSpike.LocalWebServer.exe
