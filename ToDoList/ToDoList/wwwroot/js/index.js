const taskList = document.querySelector('.task-list');
const newTaskInput = document.querySelector('input[type="text"]');

newTaskInput.addEventListener('keydown', function(event) {
	if (event.code === 'Enter' && event.target.value.trim() !== '') {
		addNewTask(event.target.value.trim());
		event.target.value = '';

        
	}
});

function addNewTask(task) {
	const taskItem = document.createElement('li');
	taskItem.innerText = task;
	taskList.appendChild(taskItem);

	taskItem.addEventListener('click', function() {
		taskItem.classList.toggle('completed');
	});

	const deleteButton = document.createElement('button');
	deleteButton.innerText = 'Delete';
	taskItem.appendChild(deleteButton);

	deleteButton.addEventListener('click', function() {
		taskItem.remove();
	});
}