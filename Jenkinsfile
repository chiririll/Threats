final DEFAULT = 'default'
final ANY = 'any'
final ALL = 'all'

pipeline {
    agent any

    parameters {
        string(name: 'BRANCH_NAME', defaultValue: 'build')
        choice(name: 'FRAMEWORK', choices: [DEFAULT, 'net6.0', 'net8.0'], description: 'Задать конкретную версию .NET')
        choice(name: 'TARGET_CONFIGURATION', choices: ['Release', 'Debug'])
        choice(name: 'TARGET_OS', choices: [ALL, 'win', 'linux'])
        choice(name: 'TARGET_ARCH', choices: [ALL, 'x64', 'x86'])
    }

    stages {
        stage('Fetch Repository') {
            sh('git fetch -p')
            sh("git reset --hard origin/${BRANCH_NAME}")
            sh("git reset --hard origin/${BRANCH_NAME}")
        }
        stage('Build Windows') {
            when {
                expression {
                    return params.TARGET_OS == ANY || params.TARGET_OS == 'win'
                }
            }

            steps {
                dir(env.WORKSPACE) {
                    sh """ dotnet publish \
                            --framework ${params.FRAMEWORK}
                            --configuration ${params.TARGET_CONFIGURATION} \
                            --os ${params.TARGET_OS}\
                            --arch ${params.TARGET_ARCH}"""
                    }
                }
            }
        }
    }
