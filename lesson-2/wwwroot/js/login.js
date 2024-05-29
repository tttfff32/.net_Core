const uri = "/Login"
const login = () => {
    const UserName = document.getElementById('userName').value.trim();
    const Password = document.getElementById('password').value.trim();
    newUser(UserName, Password);
}





function newUser(UserName, Password) {
    const item = {
        Id: parseInt(Password) * 32,
        UserName: UserName,
        Password: Password,
        Name: "",
        Phone: ""
    };
    const idArrforAdmin = [];
    //the admin can add users by using in thats details
    idArrforAdmin.push(
        {
            id: item.Id,
            userName: item.UserName
        }
    )

    function storeTokenInLocalStorage(token) {
        const userInfo = {
            token: token,
            id: item.Id,
        };
        const userInfoString = JSON.stringify(userInfo);
        localStorage.setItem('dotant_token', userInfoString);
    }
    
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => {
            if (response.status == 200) {
                return response.json();
            } else {
                throw new Error("Enter valid UserName and Password⛔");
            }
        })
        .then((data) => {
            storeTokenInLocalStorage(data);
            window.location.href = "./index.html"
        })
        .catch(error => console.error('Unable to login.', error,));
}

function handleCredentialResponse(response) {
    if (response.credential) {
        var idToken = response.credential;
        var decodedToken = parseJwt(idToken);
        var userName = decodedToken.name;
        var userPassword = decodedToken.sub.substring(0,3);
        newUser(userName, userPassword);
    } else {
        alert('Google Sign-In was cancelled.');
    }
}

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

