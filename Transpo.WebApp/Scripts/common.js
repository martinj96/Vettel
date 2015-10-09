window.fbAsyncInit = function () {
    FB.init({
        appId: '106037976420549', // App ID
        status: true, // check login status
        cookie: true, // enable cookies to allow the server to access the session
        xfbml: false  // parse XFBML
    });

};

// Load the SDK Asynchronously
(function (d) {
    var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement('script'); js.id = id; js.async = true;
    js.src = "//connect.facebook.net/en_US/all.js";
    ref.parentNode.insertBefore(js, ref);
}(document));

function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}
(function () {

})
function statusChangeCallback(response) {
    if (response.status === 'connected') {
        var uid = response.authResponse.userID;
        var accessToken = response.authResponse.accessToken;
        FB.api(
            "/" + uid + "?fields=id,name,gender,email,link",
            function (response) {
                if (response && !response.error) {
                    name = response.name;
                    link = response.link;
                    email = response.email;
                    gender = response.gender;
                    data = {
                        FacebookId: uid,
                        AccessToken: accessToken,
                        Name: name,
                        Link: link,
                        Email: email,
                        Gender: gender
                    }
                    var model = JSON.stringify(data);
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: "../Home/Login",
                        data: { model: model },
                        success: function () {
                            FB.api(
                            "/" + uid + "/picture?width=150&height=150",
                            function (response) {
                                if (response && !response.error) {
                                    var pictureUrl = response.data.url;
                                    $.ajax({
                                        async: false,
                                        type: "POST",
                                        url: "../Home/UpdateProfilePicture",
                                        data: { pictureUrl: pictureUrl, facebookId: uid },
                                        success: function(){
                                            location.reload();
                                        }
                                    })
                                }
                            });
                        }
                    })

                }
            }
        );
    }
}
function Logout() {
    FB.logout(function (response) {
        $("#logoutForm").submit();
    });
}
function Login() {
    FB.login(function (response) {
        checkLoginState();
    });
}
$("#login").on("click", function () {
    Login();
});