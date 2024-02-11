pipeline {
    agent any
    
    stages {
        stage('Clone repository') {
            steps {
                // Clean workspace before cloning
                deleteDir()

                // Clone the GitHub repository
                git 'https://github.com/ramibelhadjsalem/intershipJenkins.git'
            }
        }
       
        stage('Build Docker image') {
            steps {
                // Build Docker image using Dockerfile
                script {
                    dockerImage = docker.build('image_name', '-f Dockerfile .')
                }
            }
        }
        stage('Test') {
            steps {
                // Run tests using dotnet test
                bat 'dotnet test'
            }
        }


        stage('Run Docker container') {
            steps {
                // Run Docker container from the built image
                script {
                    dockerImage.run('-p 8089:80') 
                }
            }
        }
    }

    
}
