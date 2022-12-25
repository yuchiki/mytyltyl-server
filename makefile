
.PHONY: build
build:
	dotnet publish -o /usr/local/bin/mytyltyl-server

.PHONY: deploy
deploy:
	ansible-playbook -i ansible/inventory --private-key=~/.ssh/mytyltyl.pem ansible/deploy-mytyltyl-server.yaml
