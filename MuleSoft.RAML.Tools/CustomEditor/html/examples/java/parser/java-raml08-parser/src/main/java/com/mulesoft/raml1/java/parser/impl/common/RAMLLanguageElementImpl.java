package com.mulesoft.raml1.java.parser.impl.common;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.core.JavaElementNode;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.MarkdownString;
import com.mulesoft.raml1.java.parser.impl.systemTypes.MarkdownStringImpl;



public class RAMLLanguageElementImpl extends JavaElementNode implements RAMLLanguageElement {

    public RAMLLanguageElementImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected RAMLLanguageElementImpl(){
        super();
    }


    @XmlElement(name="description")
    public MarkdownString description(){
        return super.getAttribute("description", MarkdownStringImpl.class);
    }
}