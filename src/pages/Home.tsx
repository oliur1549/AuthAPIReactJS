
const Home = (props : {name: string}) => {
    
    return (
        <div>
           {props.name ? 'Login as ' + props.name : 'You are not logged in!'}
        </div>
    );
};

export default Home;