import React from 'react';
import { useAuthContext } from '../AuthContext';

const TaskRow = ({ task, onDoTaskClick, onCompleteTaskClick}) => {
    const {  title, userID, userName } = task;
    const { user } = useAuthContext();

    const taskAvailable = !userID;
    const myTask = user.id === userID;
    const taskTooken = !taskAvailable && !myTask;

    return (
        <tr>
            <td>{title}</td>
            <td>
                {taskAvailable && <button className='btn btn-info' onClick={onDoTaskClick}>I'm doing this!</button>}
                {myTask && <button className='btn btn-success' onClick={onCompleteTaskClick}>Done!</button>}
                {taskTooken && <button className='btn btn-warning' disabled={true}>{userName} is doing this</button>}
            </td>
        </tr>
        )
}

export default TaskRow;