
var app = angular.module('KarmaloopChatUI', ['ngMaterial', 'ngMessages'])
    .config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('altTheme')
            .primaryPalette('purple');
    })
    .constant('apiUrl', 'http://localhost:8880/api/Conversation/')
    .constant('botname', 'Gina')
    .controller('ChatUiController', ['$scope', '$http', '$mdDialog', 'apiUrl', 'botname',
        function ($scope, $http, $mdDialog, apiUrl, botname) {
            $scope.botname = botname;
            $scope.message = '';
            $scope.messages = [
                {
                    who: botname,
                    isReply: true,
                    message: "Say something to me..."
                }
            ];

            $scope.showAlert = function () {

                $mdDialog.show(
                    $mdDialog.alert()
                        .parent(angular.element(document.querySelector('#main')))
                        .clickOutsideToClose(true)
                        .title('Unable to connect to server...')
                        .textContent('It appears that the bot server is not running or is unreachable. Please check your internet connection and server status.')
                        .ariaLabel('Error connecting')
                        .ok('OK')
                );
            };

            var sendToServer = function () {
                var kluid = localStorage.getItem('kluid');

                $http.post(apiUrl + kluid, {
                    Sentence: $scope.message
                }).then(function success(response) {
                    $scope.messages.push({
                        who: botname,
                        isReply: true,
                        message: response.data.ResponseText
                    });

                    $scope.message = '';

                    setTimeout(function () {
                        var element = document.getElementById('chatMessages');
                        element.scrollTop = element.scrollHeight;
                    }, 200);

                    setTimeout(function () {                 
                        var element = document.getElementsByClassName("loading");
                      
                        for (var i = 0; i < element.length; i++) {
                            element[i].style.visibility = "hidden";
                        }
                    }, 1500);
                    setTimeout(function () {
                        var element2 = document.getElementsByClassName("show");
                        for (var i = 0; i < element2.length; i++) {
                             element2[i].style.visibility = "visible";
                        }
                    }, 1500);
                 
                }, function error(response) {
                        $scope.showAlert();
                    });

            };

            $scope.send = function () {
                if ($scope.message === '')
                    return;

                $scope.messages.push({
                    who: 'Me',
                    isReply: false,
                    message: $scope.message
                });

                var kluid = localStorage.getItem('kluid');
              
                if (!kluid) {    //If we have no kluid (which is nothing but a user ID as a GUID) then get a new one.
                    $http({
                        method: 'GET',
                        url: apiUrl + 'new'
                    }).then(function success(response) {
                        if (response.data.UserID) {
                            localStorage.setItem('kluid', response.data.UserID);
                            sendToServer();
                            }
                        }, function error(response) {
                        $scope.showAlert();
                    });
                } else {
                    sendToServer();
                }

            };
        
           
        }]);
