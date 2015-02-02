FROM mono:latest
ADD WebSocketSpike WebSocketSpike
RUN xbuild /WebSocketSpike/WebSocketSpike.sln

EXPOSE 1337
CMD mono /WebSocketSpike/WebSocketSpike.LocalWebServer/bin/Debug/WebSocketSpike.LocalWebServer.exe
