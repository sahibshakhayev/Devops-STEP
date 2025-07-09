import { useState } from 'react';

export function ProductForm({ onSubmit, initial }) {
    const [name, setName] = useState(initial?.name || '');
    const [price, setPrice] = useState(initial?.price || 0);
    return (
        <form onSubmit={e => { e.preventDefault(); onSubmit({ name, price: parseFloat(price) }); }}>
            <input placeholder="Name" value={name} onChange={e => setName(e.target.value)} required />
            <input placeholder="Price" type="number" value={price} onChange={e => setPrice(e.target.value)} required />
            <button type="submit">Save</button>
        </form>
    );
}
