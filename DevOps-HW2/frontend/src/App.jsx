import { useEffect, useState } from 'react';
import { ProductForm } from './components/ProductForm';

const API = 'http://localhost:5000/api/products';

export default function App() {
    const [products, setProducts] = useState([]);
    const [edit, setEdit] = useState(null);

    const fetchAll = () => {
        fetch(API).then(res => res.json()).then(setProducts);
    };

    useEffect(fetchAll, []);

    const add = data => {
        fetch(API, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(data) })
            .then(fetchAll);
    };

    const update = data => {
        fetch(`${API}/${edit.id}`, { method: 'PUT', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(data) })
            .then(() => { setEdit(null); fetchAll(); });
    };

    const remove = id => {
        fetch(`${API}/${id}`, { method: 'DELETE' }).then(fetchAll);
    };

    return (
        <div>
            <h1>Products</h1>
            <ProductForm onSubmit={edit ? update : add} initial={edit} />
            <ul>
                {products.map(p => (
                    <li key={p.id}>
                        {p.name} — ${p.price.toFixed(2)}{' '}
                        <button onClick={() => setEdit(p)}>Edit</button>
                        <button onClick={() => remove(p.id)}>Del</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
