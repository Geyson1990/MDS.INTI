import React from 'react'
import { Formik } from 'formik'
import validate from '../../../../utils/validate'
import getValidationSchema from '../../../../utils/getValidationSchema'
import { Button, Card, CardBody, CardHeader, Col, Form, FormFeedback, FormGroup, Input, Label, Row } from 'reactstrap';

const initialValues = {
    NumeroDocumento: '',
    NombresCompleto: ''
}

const FrmBusqueda = () => {
    return (
        <div className="animated fadeIn">
            <Card>
                <CardHeader>
                    <i className="icon-note"></i><strong>Búsqueda de personal</strong>
                </CardHeader>
                <CardBody>
                    <Formik
                        initialValues={initialValues}
                        validate={validate(getValidationSchema)}
                        onSubmit={onSubmit}
                        render={FrmBusquedaDesign}
                    />
                </CardBody>
            </Card>
        </div>
    )
}

const onSubmit = (values, { setSubmitting, setErrors }) => {
    setTimeout(() => {
        console.log('User has been sucessfully saved!', values)
        setSubmitting(false)
    }, 2000)
}

const FrmBusquedaDesign = (props) => {
    const { isSubmitting, errors, handleChange, handleSubmit } = props

    return (
        <Form className="form-horizontal" autoComplete="off">
            <Row>
                <Col md="6">
                    <FormGroup >
                        <Label htmlFor="name">Número de documento</Label>
                        <Input
                            type="text"
                            id="NumeroDocumento"
                            name="NumeroDocumento"
                            placeholder="Ingrese Número de Documento"
                            //onInput={onHandleChangeNumeric}
                            minLength="8"
                            maxLength="14"
                            onChange={handleChange}
                        //value={values.NumeroDocumento}
                        />
                        <FormFeedback>{errors.NumeroDocumento}</FormFeedback>
                    </FormGroup>
                </Col>
                <Col md="6">
                    <FormGroup>
                        <Label htmlFor="name">Apellidos y Nombres</Label>
                        <Input
                            type="text"
                            id="NombreCompleto"
                            name="NombreCompleto"
                            placeholder="Ingrese Apellidos y Nombres"
                            minLength="3"
                            maxLength="150"
                            onChange={handleChange}
                        //value={values.NombreCompleto}
                        />
                        <FormFeedback>{errors.NombreCompleto}</FormFeedback>
                    </FormGroup>
                </Col>
            </Row>
            <Row>
                <Col sm="12" style={{ textAlign: 'center' }}>
                    <Button type="submit" color="primary" onClick={handleSubmit}><i className="fa fa-search"></i> {isSubmitting ? 'Buscando...' : 'Buscar'}</Button>{' '}
                    <Button type="reset" color="secondary"><i className="fa fa-eraser"></i> Limpiar</Button>
                </Col>
            </Row>
        </Form>

    )
}

export default FrmBusqueda;