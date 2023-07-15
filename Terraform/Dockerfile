FROM hashicorp/terraform AS terraform
COPY . /infrastructure
WORKDIR /infrastructure
ENTRYPOINT [ "/bin/sh", "-c", "terraform init && terraform apply" ]
