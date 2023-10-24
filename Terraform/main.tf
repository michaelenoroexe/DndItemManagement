terraform {
  required_version = ">=1.0.0, < 2.0.0"
  backend "s3" {
    bucket = "dnd-item-manager-terraform-state"
    key    = "global/s3/actions/terraform.tfstate"
    region = "eu-north-1"

    dynamodb_table = "dnd-item-manager-terraform-lock"
    encrypt        = true
  }
  required_providers {
    aws = {
      source = "hashicorp/aws"
    }
  }
}

provider "aws" {
  region = var.target_region
}

resource "aws_launch_configuration" "workHub" {
  instance_type          = "t3.medium"
  availability_zone      = var.target_region
  ami                    = "ami-0963fd9d8200e9075"
  vpc_security_group_ids = [aws_security_group.global_group_for_proxy.id]
}

resource "aws_autoscaling_group" "name" {
  launch_configuration = aws_launch_configuration.workKub.name

  min_size = 1
  max_size = 3

  tag {
    key                 = "KubWorker"
    value               = "kub-worker"
    propagate_at_launch = true
  }
}

resource "aws_instance" "kuberDeploy" {
  instance_type     = "t3.medium"
  availability_zone = var.target_region
  ami               = "ami-0963fd9d8200e9075"

  user_data = <<-EOF
              #!bin/bash
              sudo apt-get update
              sudo apt-get install -y ca-certificates curl
              curl -fsSL https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo gpg --dearmor -o /etc/apt/keyrings/kubernetes-archive-keyring.gpg
              echo "deb [signed-by=/etc/apt/keyrings/kubernetes-archive-keyring.gpg] https://apt.kubernetes.io/ kubernetes-xenial main" | sudo tee /etc/apt/sources.list.d/kubernetes.list
              sudo apt-get update
              sudo apt-get install -y kubectl
              EOF

  user_data_replace_on_change = true
  vpc_security_group_ids      = [aws_security_group.global_group_for_proxy.id]
}

resource "aws_instance" "revers_proxy" {
  instance_type          = "t2.micro"
  availability_zone      = var.target_region
  ami                    = "ami-0110d1b5b1cdd8780"
  vpc_security_group_ids = [aws_security_group.global_group_for_proxy.id]

  user_data = <<-EOF
              #!bin/bash
              docker run \
              -p 80:80
              --name Client
              791958344472.dkr.ecr.eu-north-1.amazonaws.com/dnd-item-manager-client
              EOF
}
