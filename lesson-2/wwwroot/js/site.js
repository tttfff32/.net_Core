const uri = '/ListToDo';
let tasks = [];

function getTokenFromLocalStorage() {
    return localStorage.getItem('dotant_token');
}
const token = getTokenFromLocalStorage();
if (!token) {
    window.location.href = "./login.html";
}
const tokenObj = JSON.parse(token);
let tokenPayload;
document.addEventListener("DOMContentLoaded", function () {
    const tokenParts = tokenObj.token.split('.');
    if (tokenParts.length === 3) {
        tokenPayload = JSON.parse(atob(tokenParts[1]));
        console.log(`tokenPayload: ${tokenPayload} tokenPayload.type: ${tokenPayload.type}`);
        if (tokenPayload.type[0] === "Admin" && tokenPayload.type[1] === "classUser") {
            document.getElementById("usersButton").style.display = 'block';
        }
        else
            document.getElementById("usersButton").style.display = 'none';
    }
}
);
function getItems() {
    fetch(uri, {
        headers: {
            'Accept': 'application/json',
            'Authorization': `Bearer ${tokenObj.token}`
        }
    })
        .then(response => response.json())
        .then(data => {
            if (tokenPayload.type[0] === "Admin" && tokenPayload.type[1] === "classUser")
                getAllTasksToAdmin(data);
           else if (tokenPayload.type === "classUser") {
                _displayItems(data)
            }
        }
        )

        .catch(error => console.error('Unable to get items.', error));
}



function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const item = {
        userId: tokenObj.id,
        isCompleted: false,
        name: addNameTextbox.value.trim(),
    };
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${tokenObj.token}`
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            // addhourTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${tokenObj.token}`
        },
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
   
    const item = tasks.find(item => item.id === id );

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isCompleted').checked = item.isCompleted;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        isCompleted: document.getElementById('edit-isCompleted').checked,
        name: document.getElementById('edit-name').value.trim(),
        userId: tokenObj.id,

    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${tokenObj.token}`
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayItems(data) {
    const tBody = document.getElementById('Tasks');
    tBody.innerHTML = '';
    data.forEach(task => {
        if (task.userId == tokenObj.id) {
            const button = document.createElement('button');
            let isComplitedCheckbox = document.createElement('input');
            isComplitedCheckbox.type = 'checkbox';
            isComplitedCheckbox.disabled = true;
            isComplitedCheckbox.checked = task.isCompleted;


            let editButton = button.cloneNode(false);
            editButton.innerText = 'Edit';
            editButton.setAttribute('onclick', `displayEditForm(${task.id})`);

            let deleteButton = button.cloneNode(false);
            deleteButton.innerText = 'Delete';
            deleteButton.setAttribute('onclick', `deleteItem(${task.id})`);

            let tr = tBody.insertRow();

            let td1 = tr.insertCell(0);
            td1.appendChild(isComplitedCheckbox);

            let td2 = tr.insertCell(1);
            let textNode = document.createTextNode(task.name);
            td2.appendChild(textNode);

            let td4 = tr.insertCell(2);
            td4.appendChild(editButton);

            let td5 = tr.insertCell(3);
            td5.appendChild(deleteButton);


        }
    });
    tasks = data;
}
    function getAllTasksToAdmin(data) {
        const tBody = document.getElementById('Tasks');
        tBody.innerHTML = '';
        data.forEach(task => 
            {
                const button = document.createElement('button');
                let isComplitedCheckbox = document.createElement('input');
                isComplitedCheckbox.type = 'checkbox';
                isComplitedCheckbox.disabled = true;
                isComplitedCheckbox.checked = task.isCompleted;

                let tr = tBody.insertRow();

                let td1 = tr.insertCell(0);
                td1.appendChild(isComplitedCheckbox);

                let td2 = tr.insertCell(1);
                let textNode = document.createTextNode(task.name);
                td2.appendChild(textNode);
        });
        tasks = data;
    }

