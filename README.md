# Tracking-Project-Manager-Backend
Implementation of backend of the project in .NET

## Contracts

### Project

<details><summary>Summary</summary>
<p>

![image](https://user-images.githubusercontent.com/55404642/232625296-ef33d176-b24f-4559-a7b3-99f18c17ea49.png)

### Post
```json
{
  "leaderID": "string",
  "name": "string",
  "description": "string"
}
  response
{
  "leaderID": "string",
  "name": "string",
  "description": "string",
  "createdAt": "2023-04-17T22:29:34.298Z",
  "openDate": "2023-04-17T22:29:34.298Z",
  "deadLine": "2023-04-17T22:29:34.298Z",
  "completedAt": "2023-04-17T22:29:34.298Z",
  "efficiencyRate": 0,
  "phase": 0,
  "stateProject": 0
}
```
### Put (Project/ID)
```json
  projectID
{
  "leaderID": "string",
  "name": "string",
  "description": "string",
  "deadLine": "2023-04-17T22:27:23.855Z",
  "stateProject": 0
}
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "leaderID": "string",
  "name": "string",
  "description": "string",
  "createdAt": "2023-04-17T22:30:59.747Z",
  "openDate": "2023-04-17T22:30:59.747Z",
  "deadLine": "2023-04-17T22:30:59.747Z",
  "completedAt": "2023-04-17T22:30:59.749Z",
  "efficiencyRate": 0,
  "phase": 0,
  "stateProject": 0
}
```
### Delete (Project/ID)
```json
  projectID
  
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "leaderID": "string",
  "name": "string",
  "description": "string",
  "createdAt": "2023-04-17T22:31:16.693Z",
  "openDate": "2023-04-17T22:31:16.693Z",
  "deadLine": "2023-04-17T22:31:16.693Z",
  "completedAt": "2023-04-17T22:31:16.693Z",
  "efficiencyRate": 0,
  "phase": 0,
  "stateProject": 0
}
```
### Get (Project/ID)
```json
  projectID
  
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "leaderID": "string",
  "name": "string",
  "description": "string",
  "createdAt": "2023-04-17T22:31:16.693Z",
  "openDate": "2023-04-17T22:31:16.693Z",
  "deadLine": "2023-04-17T22:31:16.693Z",
  "completedAt": "2023-04-17T22:31:16.693Z",
  "efficiencyRate": 0,
  "phase": 0,
  "stateProject": 0
}
```
### Put (Project/OpenProject/ID)
```json
  projectID
{
  "deadLine": "2023-04-17T22:31:51.157Z"
}
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "leaderID": "string",
  "name": "string",
  "description": "string",
  "createdAt": "2023-04-17T22:32:52.653Z",
  "openDate": "2023-04-17T22:32:52.653Z",
  "deadLine": "2023-04-17T22:32:52.653Z",
  "completedAt": "2023-04-17T22:32:52.653Z",
  "efficiencyRate": 0,
  "phase": 0,
  "stateProject": 0
}
```
### Put (Project/OpenProject/ID)
```json
  projectID
  
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "leaderID": "string",
  "name": "string",
  "description": "string",
  "createdAt": "2023-04-17T22:34:31.628Z",
  "openDate": "2023-04-17T22:34:31.628Z",
  "deadLine": "2023-04-17T22:34:31.628Z",
  "completedAt": "2023-04-17T22:34:31.628Z",
  "efficiencyRate": 0,
  "phase": 0,
  "stateProject": 0
}
```
### Get (Project/LeaderID)
```json
  leaderID
  
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "leaderID": "string",
  "name": "string",
  "description": "string",
  "createdAt": "2023-04-17T22:31:16.693Z",
  "openDate": "2023-04-17T22:31:16.693Z",
  "deadLine": "2023-04-17T22:31:16.693Z",
  "completedAt": "2023-04-17T22:31:16.693Z",
  "efficiencyRate": 0,
  "phase": 0,
  "stateProject": 0
}
```
### Get (Project/ActiveOnly)
```json
  response
[
  {
    "projectID": "ed7d4afd-d4d5-4972-99cf-3602ef836cb5",
    "leaderID": "N7daGI7budgGdWHswJUpBmS2XJw1",
    "name": "Angular ",
    "description": "Frontend Presentacion",
    "createdAt": "2023-04-16T18:57:26.117",
    "openDate": "2023-04-12T00:00:00",
    "deadLine": "2023-04-16T00:00:00",
    "completedAt": null,
    "efficiencyRate": 0,
    "phase": 0,
    "stateProject": 0
  }
]
```
### Get (Project/AllNoDeleted)
```json
  response
[
  {
    "projectID": "ed7d4afd-d4d5-4972-99cf-3602ef836cb5",
    "leaderID": "N7daGI7budgGdWHswJUpBmS2XJw1",
    "name": "Angular ",
    "description": "Frontend Presentacion",
    "createdAt": "2023-04-16T18:57:26.117",
    "openDate": "2023-04-12T00:00:00",
    "deadLine": "2023-04-16T00:00:00",
    "completedAt": null,
    "efficiencyRate": 0,
    "phase": 0,
    "stateProject": 0
  }
]
```
</p>
</details>

### Task

<details><summary>Summary</summary>
<p>

![image](https://user-images.githubusercontent.com/55404642/232625390-46207e19-ef90-448c-9b37-330cbbec8a27.png)

### Post
```json
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "createdBy": "string",
  "deadline": "2023-04-17T22:38:49.452Z",
  "priority": 0
}
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "createdBy": "string",
  "assignedTo": "string",
  "createdAt": "2023-04-17T22:39:06.334Z",
  "assignedAt": "2023-04-17T22:39:06.334Z",
  "deadline": "2023-04-17T22:39:06.334Z",
  "completedAt": "2023-04-17T22:39:06.334Z",
  "priority": 0,
  "stateTask": 0
}
```
### Put (Task/ID)
```json
  taskID
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "assignedTo": "string",
  "assignedAt": "2023-04-17T22:39:55.444Z",
  "deadline": "2023-04-17T22:39:55.444Z",
  "priority": 0
}
  response
{
  "taskID": 0,
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "createdBy": "string",
  "assignedTo": "string",
  "createdAt": "2023-04-17T22:39:55.445Z",
  "assignedAt": "2023-04-17T22:39:55.445Z",
  "deadline": "2023-04-17T22:39:55.445Z",
  "completedAt": "2023-04-17T22:39:55.445Z",
  "priority": 0,
  "stateTask": 0
}
```
### Delete (Project/ID)
```json
  taskID
  
  response
{
  "taskID": 0,
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "createdBy": "string",
  "assignedTo": "string",
  "createdAt": "2023-04-17T22:40:19.710Z",
  "assignedAt": "2023-04-17T22:40:19.711Z",
  "deadline": "2023-04-17T22:40:19.711Z",
  "completedAt": "2023-04-17T22:40:19.711Z",
  "priority": 0,
  "stateTask": 0
}
```
### Get (Task/TaskID)
```json
  taskID
  
  response
{
  "taskID": 0,
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "createdBy": "string",
  "assignedTo": "string",
  "createdAt": "2023-04-17T22:40:19.710Z",
  "assignedAt": "2023-04-17T22:40:19.711Z",
  "deadline": "2023-04-17T22:40:19.711Z",
  "completedAt": "2023-04-17T22:40:19.711Z",
  "priority": 0,
  "stateTask": 0
}
```
### Get (Task/All)
```json
  response
[
  {
    "taskID": 0,
    "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "description": "string",
    "createdBy": "string",
    "assignedTo": "string",
    "createdAt": "2023-04-17T22:41:52.549Z",
    "assignedAt": "2023-04-17T22:41:52.549Z",
    "deadLine": "2023-04-17T22:41:52.549Z",
    "completedAt": "2023-04-17T22:41:52.549Z",
    "priority": 0,
    "stateTask": 0
  }
]
```
### Get (Task/Unassigned)
```json
  leaderID
  
  response
[
  {
    "taskID": 0,
    "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "description": "string",
    "createdBy": "string",
    "assignedTo": "string",
    "createdAt": "2023-04-17T22:41:52.549Z",
    "assignedAt": "2023-04-17T22:41:52.549Z",
    "deadLine": "2023-04-17T22:41:52.549Z",
    "completedAt": "2023-04-17T22:41:52.549Z",
    "priority": 0,
    "stateTask": 0
  }
]
```
### Get (Task/UserID)
```json
  userID
  
  response
[
  {
    "taskID": 0,
    "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "description": "string",
    "createdBy": "string",
    "assignedTo": "string",
    "createdAt": "2023-04-17T22:42:25.861Z",
    "assignedAt": "2023-04-17T22:42:25.861Z",
    "deadLine": "2023-04-17T22:42:25.861Z",
    "completedAt": "2023-04-17T22:42:25.861Z",
    "priority": 0,
    "stateTask": 0
  }
]
```
### Put (Task/CompleteTask/ID)
```json
  taskID
  
  response
{
  "taskID": 0,
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "createdBy": "string",
  "assignedTo": "string",
  "createdAt": "2023-04-17T22:44:10.756Z",
  "assignedAt": "2023-04-17T22:44:10.756Z",
  "deadline": "2023-04-17T22:44:10.756Z",
  "completedAt": "2023-04-17T22:44:10.756Z",
  "priority": 0,
  "stateTask": 0
}
```
### Put (Task/AssignTask/ID)
```json
  taskID
  userID
  
  response
{
  "taskID": 0,
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "createdBy": "string",
  "assignedTo": "string",
  "createdAt": "2023-04-17T22:44:49.528Z",
  "assignedAt": "2023-04-17T22:44:49.528Z",
  "deadline": "2023-04-17T22:44:49.528Z",
  "completedAt": "2023-04-17T22:44:49.528Z",
  "priority": 0,
  "stateTask": 0
}
```
</p>
</details>

### Inscription

<details><summary>Summary</summary>
<p>

![image](https://user-images.githubusercontent.com/55404642/232626484-a09d0e42-08ee-407e-94ed-b23f49e42653.png)

### Post
```json
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "uidUser": "string"
}
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "uidUser": "string",
  "createdAt": "2023-04-17T22:47:31.070Z",
  "stateInscription": 0
}
```
### Put (Inscription/Respond)
```json
  inscriptionID
  state
  
  response
{
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "uidUser": "string",
  "createdAt": "2023-04-17T22:47:52.081Z",
  "responsedAt": "2023-04-17T22:47:52.081Z",
  "stateInscription": 0
}
```
### Delete (Inscription/ID)
```json
  taskID
  
  response
{
  "inscriptionID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "uidUser": "string",
  "createdAt": "2023-04-17T22:49:08.949Z",
  "responsedAt": "2023-04-17T22:49:08.949Z",
  "stateInscription": 0
}
```
### Get (Inscription/UserID)
```json
  userID
  
  response
{
  "inscriptionID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "uidUser": "string",
  "createdAt": "2023-04-17T22:49:08.949Z",
  "responsedAt": "2023-04-17T22:49:08.949Z",
  "stateInscription": 0
}
```
### Get (Inscription/NoResponded)
```json
  response
[
  {
    "inscriptionID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "projectID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "uidUser": "string",
    "createdAt": "2023-04-17T22:49:55.053Z",
    "responsedAt": "2023-04-17T22:49:55.053Z",
    "stateInscription": 0
  }
]
```
</p>
</details>

Gracias :D 🚀
