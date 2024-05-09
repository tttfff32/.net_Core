const uri = "/Admin"



function newUser() {
    const UserName = document.getElementById('userName');
    const Password = document.getElementById('password');

    const item = {
        Id: parseInt(Password.value.trim())*32,
        UserName: UserName.value.trim(),
        Password: Password.value.trim(),
    };

    function storeTokenInLocalStorage(token) {
        const userInfo = {
            token: token,
            id: item.Id
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
        .catch(error => console.error('Unable to add item.', error));
}