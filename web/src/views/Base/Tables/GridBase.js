import React, { useState, useEffect } from 'react';
import { Table } from 'reactstrap';
import TableHeader from './TableHeader';
import TableData from './TableData';

const GridBase = (datos, meta) => {
    const [headerMeta, setHeaderMeta] = useState(meta);
    const [tableData, setTableData] = useState([]);
    const [sortBy, setSortBy] = useState({ key: null, order: '>' });
    const data = datos.length!==undefined ? datos.map((d, id) => ({ ...d, id })): [];
    
    const compare = {
        '>': (d1, d2) => d1 > d2,
        '<': (d1, d2) => d1 < d2,
    };

    const normalizeData = (data) => {
        return data.map(td => {
            const keys = Object.keys(td);
            return keys.map(key => ({ key, text: td[key] }));
        });
    };

    useEffect(() => {
        // normalize data
        setTableData(normalizeData(data), meta);
    }, []);

    useEffect(() => {
        // sort
        setTableData(normalizeData(data.sort((d1, d2) => compare[sortBy.order](d1[sortBy.key], d2[sortBy.key]))));
    }, [sortBy])

    return (
        <div className="container">
            <Table>
                <TableHeader headers={headerMeta} />
                <TableData data={tableData} meta={meta} />
            </Table>
        </div>
    );
}

export default GridBase;