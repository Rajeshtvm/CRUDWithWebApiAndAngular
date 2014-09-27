function friendController($scope, $http) {
    $scope.loading = true;
    $scope.addMode = false;

    $http.get('api/Friend/').success(function (data) {
        $scope.friends = data;
        $scope.loading = false;
    }).error(function () {
        $scope.error = "An error has occured while loadin post!";
        $scope.loading = false;
    });

    $scope.toggleEdit = function () {
        this.friend.editMode = !this.friend.editMode;
    };
    $scope.toggleAdd = function () {
        $scope.addMode = !this.friend.addMode;
    };

    //Upadte/Edit Records
    $scope.save = function () {
        alert("Edit");
        $scope.loading = true;
        var frien = this.friend;
        $http.put('api/Friend/', frien).success(function (data) {
            alert("Updated Successfully!!");
            $scope.loading = false;
            }).error(function(data){
                $scope.error = "An error has occured while updating friends!!" + data;
                $scope.loading = false;
            });

    };

    //Add new records

    $scope.add = function(){
        $scope.loading = true;
        $http.post('api/Friend', this.newFriend).success(function (data) {
            alert("Added successfully!!");
            $scope.addMode = false;
            $scope.friends.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Friend! " + data;
            $scope.loading = false;
        });

    };
    $scope.deletefriend = function () {
        $scope.loading = true;
        var friendid = this.friend.FriendId;
        $http.delete('/api/Friend/' + friendid).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.friends, function (i) {
                if ($scope.friends[i].FriendId === friendid) {
                    $scope.friends.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Friend! " + data;
            $scope.loading = false;

        });
    };

}