import React, { useState, useEffect } from 'react';
import { Table } from 'reactstrap';
import TableHeader from './TableHeader';
import TableData from './TableData';

const GridBase = ({datos, meta, edit, eliminar}) => {
    const [headerMeta, setHeaderMeta] = useState(meta);
    const [tableData, setTableData] = useState([]);
    
    //const data = datos.length!==undefined ? datos.map((d, id) => ({ ...d, id })): [];

    return (
        <div>
            <Table hover bordered striped responsive size="sm">
                <TableHeader key={1} headers={headerMeta} />
                <TableData key={2} data={datos} meta={meta} edit={edit} eliminar={eliminar}/>
            </Table>
        </div>
    );
}

export default GridBase;