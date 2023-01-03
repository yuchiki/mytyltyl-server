
.PHONY: build
build:
	dotnet publidh --self-contained -o out

.PHONY: deploy
deploy:
	ansible-playbook -i ansible/inventory --private-key=~/.ssh/mytyltyl.pem ansible/deploy-mytyltyl-server.yaml
