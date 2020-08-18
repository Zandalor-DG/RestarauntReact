import React from 'react';
import { Page } from './Page';
<<<<<<< HEAD
import { Form } from './Form';
import { Field } from './Field';

export const AskPage = () => (
  <Page title="Ask a question">
    <Form submitCaption="Submit Your Question">
      <Field name="title" label="Title" />
      <Field name="content" label="Content" type="TextArea" />
    </Form>
  </Page>
);
export default AskPage;
=======
export const AskPage = () => <Page title="Ask a question" />;
>>>>>>> 90439349542c8601c16e772e31756bcf7f41333b
