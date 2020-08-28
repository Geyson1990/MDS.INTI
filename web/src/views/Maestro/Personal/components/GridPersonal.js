import React from 'react';
import GridBase from "../../../Base/Tables/GridBase";
import { Card, CardBody, CardHeader } from 'reactstrap';

const GridPersonal = (props) => {
    const meta = [
        {
            key: 'N°',
            text: 'N°'
        },
        {
            key: 'NumeroDocumento',
            text: 'Número de documento'
        },
        {
            key: 'DscArea',
            text: 'Área u Oficina'
        }
    ];
    const datos = [];

    return (
        <Card>
            <CardHeader>
                <i className="icon-note"></i><strong>Resultados de búsqueda</strong>
            </CardHeader>
            <CardBody>
                <GridBase
                    datos={datos}
                    meta={meta}
                    edit={true}
                    eliminar={true}
                />
            </CardBody>
        </Card>
    );
}

export default GridPersonal;