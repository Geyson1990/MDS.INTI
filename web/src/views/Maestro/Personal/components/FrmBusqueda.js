import React from 'react'
import { Formik } from 'formik';
import * as Yup from 'yup';
import { Button, Card, CardBody, CardHeader, Col, Form, FormFeedback, FormGroup, Input, Label, Row } from 'reactstrap';
import { _maestroServiceListarPersonal } from '../../../../services/maestroService';
import './ValidationForms.css'

const validationSchema = function (values) {
    return Yup.object().shape({
        NumeroDocumento: Yup.string()
            .min(8, `El Número de documento debe contener 8 caracteres como mínimo.`),
            NombreCompleto: Yup.string()
            .min(3, `El campo nombres debe contener como mínimo 3 caracteres.`),

    })
}

const validate = (getValidationSchema) => {
    return (values) => {
        const validationSchema = getValidationSchema(values)
        try {
            validationSchema.validateSync(values, { abortEarly: false })
            return {}
        } catch (error) {
            return getErrorsFromValidationError(error)
        }
    }
}


const getErrorsFromValidationError = (validationError) => {
    const FIRST_ERROR = 0
    return validationError.inner.reduce((errors, error) => {
        return {
            ...errors,
            [error.path]: error.errors[FIRST_ERROR],
        }
    }, {})
}

const initialValues = {
    NumeroDocumento: "",
    NombreCompleto: ""
}

const onSubmit = (values, { setSubmitting, setErrors }) => {
    setTimeout(() => {
        alert(JSON.stringify(values, null, 2))
        // console.log('User has been successfully saved!', values)
        setSubmitting(false)
    }, 2000)
}

const FrmBusqueda = (props) => {


    const findFirstError = (formName, hasError) => {
        const form = document.forms[formName]
        for (let i = 0; i < form.length; i++) {
            if (hasError(form[i].name)) {
                form[i].focus()
                break
            }
        }
    }

    const validateForm = (errors) => {
        findFirstError('simpleForm', (fieldName) => {
            return Boolean(errors[fieldName])
        })
    };

    const touchAll = (setTouched, errors) => {
        setTouched({
            NumeroDocumento: true,
            NombreCompleto: true
        }
        )
        validateForm(errors);
    };



    return (
        <div className="animated fadeIn">
            <Card>
                <CardHeader>
                    <i className="icon-note"></i><strong>Búsqueda de personal</strong>
                </CardHeader>
                <CardBody>
                    <Formik
                        initialValues={initialValues}
                        validate={validate(validationSchema)}
                        onSubmit={onSubmit}
                        render={
                            ({
                                values,
                                errors,
                                touched,
                                status,
                                dirty,
                                handleChange,
                                handleBlur,
                                handleSubmit,
                                isSubmitting,
                                isValid,
                                handleReset,
                                setTouched
                            }) => (
                                    <Form className="form-horizontal" onSubmit={handleSubmit} autoComplete="off" noValidate name='simpleForm'>
                                        <Row>
                                            <Col md="6">
                                                <FormGroup >
                                                    <Label htmlFor="name">Número de documento</Label>
                                                    <Input
                                                        type="text"
                                                        id="NumeroDocumento"
                                                        name="NumeroDocumento"
                                                        placeholder="Ingrese Número de Documento"
                                                        valid={values.NumeroDocumento.trim()!=="" && !errors.NumeroDocumento}
                                                        invalid={values.NumeroDocumento.trim()!=="" && !!errors.NumeroDocumento}
                                                        maxLength="14"
                                                        autoFocus={true}
                                                        onChange={handleChange}
                                                        onBlur={handleBlur}
                                                        value={values.NumeroDocumento}
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
                                                        valid={values.NombreCompleto.trim()!=="" && !errors.NombreCompleto}
                                                        invalid={values.NombreCompleto.trim()!=="" && !!errors.NombreCompleto}
                                                        onChange={handleChange}
                                                        onBlur={handleBlur}
                                                        value={values.NombreCompleto}
                                                    />
                                                    <FormFeedback>{errors.NombreCompleto}</FormFeedback>
                                                </FormGroup>
                                            </Col>
                                        </Row>
                                        <Row>
                                            <Col sm="12" style={{ textAlign: 'center' }}>
                                                <Button type="submit" color="primary" disabled={!isValid}><i className="fa fa-search"></i> {'Buscar'}</Button>{' '}
                                                <Button type="reset" color="secondary"><i className="fa fa-eraser"></i> Limpiar</Button>
                                            </Col>
                                        </Row>
                                    </Form>
                                )} />
                </CardBody>
            </Card>
        </div>
    )
}

export default FrmBusqueda;