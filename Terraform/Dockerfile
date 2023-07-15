FROM hashicorp/terraform AS terraform
COPY . /infrastructure
WORKDIR /infrastructure
CMD [ "/bin/sh", "-c", "terraform init && terraform apply" ]
