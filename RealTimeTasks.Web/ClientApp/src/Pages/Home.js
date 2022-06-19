import React, { useEffect, useState, useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import TaskRow from '../Components/TaskRow';


const Home = () => {
    const [title, setTitle] = useState('')
    const [tasks, setTasks] = useState([]);
    const connectionRef = useRef(null);

    useEffect(() => {
        const connectToHub = async () => {
            const connection = new HubConnectionBuilder().withUrl("/tasks").build();
            await connection.start();
            
            connectionRef.current = connection;

            connection.on('RenderTasks', tasks => {
                setTasks(tasks);
            });

            connection.invoke('UserLogedIn');
        }
        connectToHub();
    }, []);

    const onDoTaskClick = async (taskID) => {
        connectionRef.current.invoke('dotask', taskID );
    }

    const onCompleteTaskClick = async (taskID) => {
        connectionRef.current.invoke('completetask', taskID );
    }
    const onAddClick = async () => {
        connectionRef.current.invoke('addtask', { title });
        setTitle('');
    }

    return (<div className='container'>
        <div className='row mt-5'>
            <div className='col-md-10'>
                <input type='text' placeholder='Task Title' className='form-control'
                    value={title} onChange={e => setTitle(e.target.value)} />
            </div>
            <div className='col-md-2'>
                <button className='btn btn-primary btn-block' onClick={onAddClick}>Add Task</button>
            </div>
        </div>
        <table className='table table-hover table-striped table-bordered mt-3'>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                {tasks.map(t => <TaskRow
                    task={t}
                    key={t.id}
                    onDoTaskClick={() => onDoTaskClick(t.id)}
                    onCompleteTaskClick={() => onCompleteTaskClick(t.id)}/>)}
            </tbody>
        </table>
    </div>
    )
}

export default Home;