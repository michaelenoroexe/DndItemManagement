FROM hashicorp/terraform AS terraform
COPY . /infrastructure
WORKDIR /infrastructure
CMD [ "terraform", "init" ]
