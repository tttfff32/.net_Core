const uri = '/Users';
let users = [];

function getTokenFromLocalStorage() {
    return localStorage.getItem('dotant_token');
}
const token = getTokenFromLocalStorage();
if (!token) {
    window.location.href = "./login.html";
}
const tokenObj=JSON.parse(token);
function getItems() {
   
    fetch(uri, {
        headers: {
          'Accept': 'application/json',
          'Authorization': `Bearer ${tokenObj.token}`
        }
      })
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addIdTextbox = document.getElementById('add-id')
    const addNameTextbox = document.getElementById('add-name');
     const addPhoneTextbox = document.getElementById('add-Phone');
    const item = {
         Id: addIdTextbox.value.trim(),
        Name: addNameTextbox.value.trim(),
        Phone: addPhoneTextbox.value.trim()
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
            addIdTextbox.value = ''; 
            addNameTextbox.value = '';
             addPhoneTextbox.value = '';
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
    const item = users.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-Phone').value = item.Phone;
    document.getElementById('edit-id').value = item.Id;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        Id: parseInt(itemId, 10),
        Phone: document.getElementById('edit-Phone').value.trim(),
        Name: document.getElementById('edit-name').value.trim(),

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

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'Task' : 'Task kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('Users');
    tBody.innerHTML = '';
    data.forEach(user => {
     
                _displayCount(data.length);
                const button = document.createElement('button');
                    // let isComplitedCheckbox = document.createElement('input');
                    // isComplitedCheckbox.type = 'checkbox';
                    // isComplitedCheckbox.disabled = true;
                    // isComplitedCheckbox.checked = task.isCompleted;
            
            
                    let editButton = button.cloneNode(false);
                    editButton.innerText = 'Edit';
                    editButton.setAttribute('onclick', `displayEditForm(${user.id})`);
            
                    let deleteButton = button.cloneNode(false);
                    deleteButton.innerText = 'Delete';
                    deleteButton.setAttribute('onclick', `deleteItem(${user.id})`);
            
                    let tr = tBody.insertRow();
            
                    // let td1 = tr.insertCell(0);
                    // td1.appendChild(isComplitedCheckbox);
            
                    let td2 = tr.insertCell(0);
                    let textNode = document.createTextNode(task.name);
                    td2.appendChild(textNode);
            
                    let td3 = tr.insertCell(1);
                    let PhoneNode = document.createTextNode(item.Phone);
                    td3.appendChild(PhoneNode);
            
                    let td4 = tr.insertCell(2);
                    td4.appendChild(editButton);
            
                    let td5 = tr.insertCell(3);
                    td5.appendChild(deleteButton);
                
            
            
        });
       
    users = data;
}
