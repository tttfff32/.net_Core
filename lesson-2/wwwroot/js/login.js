const uri = "/Login"


const idArrforAdmin =[];
function newUser() {
    const UserName = document.getElementById('userName');
    const Password = document.getElementById('password');

    const item = {
        Id: parseInt(Password.value.trim())*32,
        UserName: UserName.value.trim(),
        Password: Password.value.trim(),
        Name:"",
        Phone:""
    };
    //the admin can add users by using in thats details
    idArrforAdmin.push(
        {id:item.id,
        userName:item.UserName
    }
)
    function storeTokenInLocalStorage(token,isAdmin) {
        const userInfo = {
            token: token,
            id: item.Id,
            isAdmin:isAdmin
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
        .then(response => response.json())
        .then((data) => {
            storeTokenInLocalStorage(data);
            window.location.href = "./index.html"
        })
        .catch(error => console.error('Unable to login.', error, ));
        // window.location.href="./login.html"
}

function openPostman() {
    window.open('postman://app', '_blank');
}