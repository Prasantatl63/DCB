pipeline {
    agent any

    environment {
        KUBECONFIG = '/var/jenkins_home/.kube/config'
        JOB_NAME_K8S = 'dcb-playwright'
        TEST_IMAGE = 'dcb:1.0'
    }

    stages {

        stage('Verify Docker') {
            steps {
                sh '''
                    echo "===== Docker Version ====="
                    docker --version

                    echo "===== Docker Containers ====="
                    docker ps
                '''
            }
        }

        stage('Verify Kubernetes') {
            steps {
                sh '''
                    echo "===== Kubernetes Nodes ====="

                    kubectl \
                      --kubeconfig=$KUBECONFIG \
                      --insecure-skip-tls-verify=true \
                      get nodes
                '''
            }
        }

        stage('Build Test Image') {
            steps {
                sh '''
                    echo "===== Building Test Image ====="

                    docker build -t ${TEST_IMAGE} .
                '''
            }
        }

        stage('Deploy Parallel Test Shards') {
            steps {
                sh '''
                    echo "===== Removing Previous Job ====="

                    kubectl \
                      --kubeconfig=$KUBECONFIG \
                      --insecure-skip-tls-verify=true \
                      delete job ${JOB_NAME_K8S} \
                      --ignore-not-found=true

                    echo "===== Deploying 3 Parallel Shards ====="

                    kubectl \
                      --kubeconfig=$KUBECONFIG \
                      --insecure-skip-tls-verify=true \
                      apply -f dcb-sharded-job.yaml
                '''
            }
        }

        stage('Monitor Test Execution') {
            steps {
                sh '''
                    echo "===== Waiting for 3 Shards ====="

                    kubectl \
                      --kubeconfig=$KUBECONFIG \
                      --insecure-skip-tls-verify=true \
                      wait \
                      --for=condition=complete \
                      job/${JOB_NAME_K8S} \
                      --timeout=10m
                '''
            }
        }

        stage('Display Test Results') {
            steps {
                sh '''
                    echo "=========================================="
                    echo "           TEST RESULTS"
                    echo "=========================================="

                    kubectl \
                      --kubeconfig=$KUBECONFIG \
                      --insecure-skip-tls-verify=true \
                      get jobs,pods

                    echo ""
                    echo "===== SHARD 1 ====="

                    kubectl \
                      --kubeconfig=$KUBECONFIG \
                      --insecure-skip-tls-verify=true \
                      logs \
                      -l app=dcb-playwright \
                      --prefix=true \
                      --tail=-1
                '''
            }
        }
    }

    post {
        always {
            echo '===== Jenkins Pipeline Completed ====='
        }

        success {
            echo '===== ALL SHARDS COMPLETED SUCCESSFULLY ====='
        }

        failure {
            echo '===== ONE OR MORE SHARDS FAILED ====='
        }
    }
}
