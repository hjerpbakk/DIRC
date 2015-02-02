Set up SignalR backend
===
To set up the SignalR on a Linux server using [Docker](www.docker.com)...

    gir clone https://github.com/Sankra/DIRC.git ; cd DIRC/
    ./buildAndDeploySignalRHub

Or via its own isolated [Vagrant](www.vagrantup.com) VM:

    vagrant up

In both cases, the server listens at port localhost:8080.
<br>
For more information about running .NET code on *nix via Docker, see [this post](https://gist.github.com/andmos/e8a08028fd47cd287e84#file-build-test-and-deploy-net-apps-with-vagrant-and-docker-md).
