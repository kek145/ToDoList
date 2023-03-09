async function getAllTask(){
    const response = await fetch("/api/ToDo", {
        method: "GET",
        headers: {"Accept": "application/json", "Content-Type": "application/json"}
    });

    if(response.ok === true){
        const tasks = await response.json();
        const rows = document.querySelector("tbody");
        tasks.forEach(user => rows.append(row(user)));
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

async function getTask(id){
    const response = await fetch(`/api/ToDo/${id}`, {
        method: "GET",
        headers: {"Accept": "application/json"},
    });

    if(response.ok === true){
        const task = await response.json();
        document.getElementById("userId").value = task.id;
        document.getElementById("taskName").value = task.taskName;
        document.getElementById("description").value = task.description;
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

async function createTask(TaskName, Description){
    const response = await fetch("/api/ToDo", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            taskName: TaskName,
            description: Description,
        })
    });

    if(response.ok === true){
        const task = await response.json();
        document.querySelector("tbody").append(row(task));
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

async function editTask(userId, TaskName, Description){
    const response = await fetch("/api/ToDo", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: userId,
            taskName: TaskName,
            description: Description,
        })
    });

    if(response.ok === true){
        const task = await response.json();
        document.querySelector(`tr[data-rowid='${user.id}']`).replaceWith(row(task));
    }
    else{
        const error = await response.json();
        console.log(error.message);
    }
}

async function deleteTask(id){
    const response = await fetch("/api/ToDo", {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });

    if(response.ok === true){
        const task = await response.json();
        document.querySelector(`tr[data-rowid='${user.id}']`).remove();
    }
    else{
        const error = await response.json();
        console.log(error.message);
    }
}

function reset(){
    document.getElementById("taskName").value = "";
    document.getElementById("description").value = "";
}

function row(task){
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", task.id);
  
    const taskTd = document.createElement("td");
    taskTd.append(task.taskName);
    tr.append(taskTd);
  
    const descriptionTd = document.createElement("td");
    descriptionTd.append(task.description);
    tr.append(descriptionTd);

    const dateCreationTd = document.createElement("td");
    dateCreationTd.append(task.DateCreation);
    tr.append(descriptionTd);
  
    const linksTd = document.createElement("td");
  
    const editLink = document.createElement("button"); 
    editLink.append("Change");
    editLink.addEventListener("click", async() => await getUser(task.id));
    linksTd.append(editLink);
  
    const removeLink = document.createElement("button"); 
    removeLink.append("Delete");
    removeLink.addEventListener("click", async () => await deleteUser(task.id));
  
    linksTd.append(removeLink);
    tr.appendChild(linksTd);
  
    return tr;
}

document.getElementById("resetBtn").addEventListener("click", () =>  reset());

document.getElementById("saveBtn").addEventListener("click", async () => {
        const id = document.getElementById("userId").value;
        const taskName = document.getElementById("taskName").value;
        const description = document.getElementById("description").value;

        if (id === "")
            await createUser(taskName, description);
        else
            await editUser(id, taskName, description);
        reset();
});

getAllTask();